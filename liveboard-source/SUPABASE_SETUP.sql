-- Run this in Supabase Dashboard -> SQL Editor.
-- The app lists public.users.username and public.users.avatar_url only.
-- Every authenticated user can see this directory because the screenshot's
-- table has no user_id/owner column. Passwords are never queried or granted.

alter table public.users enable row level security;

revoke all privileges on table public.users from public, anon, authenticated;
revoke select (username, password, avatar_url)
  on table public.users from public, anon, authenticated;

grant usage on schema public to authenticated;
grant select (username, avatar_url) on table public.users to authenticated;

drop policy if exists "Authenticated users can read user profiles"
  on public.users;
create policy "Authenticated users can read user profiles"
  on public.users for select to authenticated
  using (true);

do $$
begin
  if not exists (
    select 1
    from pg_publication_tables
    where pubname = 'supabase_realtime'
      and schemaname = 'public'
      and tablename = 'users'
  ) then
    execute 'alter publication supabase_realtime add table public.users';
  end if;
end;
$$;
