# Liveboard — Flutter Web + Supabase

Minimal Flutter Web login with Google/Gmail, GitHub, Apple, Yandex and VK. After login it shows the Supabase Auth user's name, the profile rows from `public.users`, and a sign-out button.

## Run locally

The project URL is prefilled in `env.example.json`. Copy it to `env.json` and paste the full **Publishable key** from Supabase into that file. Do not use a value from **Secret keys**.

```powershell
flutter pub get
flutter run -d chrome --dart-define-from-file=env.json
```

For a production web build:

```powershell
flutter build web --release --dart-define-from-file=env.json
```

The deployed site is available under `/liveboard/` on the existing GitHub Pages site. `web/flutter_bootstrap.js` points CanvasKit to the matching files already served from `/canvaskit/` at the site root. OAuth returns to the current page path, so add `https://r1go0.github.io/liveboard/` to **Supabase → Authentication → URL Configuration → Redirect URLs** after publishing.

Do not open `web/index.html` by double-clicking it. Flutter must serve the app over HTTP; `flutter run` does this for development. The production files are generated in `build/web` and must also be hosted over HTTP/HTTPS. Never put a Supabase secret or `service_role` key in a browser app.

## Supabase database setup

1. In **Authentication → URL Configuration**, add the local site origin printed by Flutter and the deployed website URL to the redirect allow list.
2. Run [`SUPABASE_SETUP.sql`](SUPABASE_SETUP.sql) in **SQL Editor**. It enables RLS on your existing `public.users` table, allows authenticated users to read only `username` and `avatar_url`, and enables Realtime for the table.
3. The screenshot's `users` table has no owner column, so this setup intentionally shows the same profile directory to every signed-in user. The app requests only `username, avatar_url`; it never reads the `password` column.

The `password` column shown in the table should not contain real passwords. Social login is handled by Supabase Auth, which stores credentials separately. Migrate any real account credentials to Supabase Auth before removing that column; do not copy passwords into Flutter or expose the table through the public API.

## OAuth providers

Google and GitHub are built-in Supabase providers: enable them under **Authentication → Providers**, enter the OAuth credentials from their developer consoles, then add the Supabase callback URL shown in the dashboard to each provider's settings.

### Apple

For web OAuth, Apple requires an Apple Developer account and a Services ID associated with an App ID that has Sign in with Apple enabled. Configure the Services ID's website domain and register the Supabase callback URL shown in the Apple setup guide. In Supabase's Apple provider form, enter that Services ID as the client ID and the generated Apple client secret. Keep the `.p8` private key safe; do not add it to this project. See [Supabase's Apple setup](https://supabase.com/docs/guides/auth/social-sign-in/auth-apple).

### Yandex and VK

The buttons call `custom:yandex` and `custom:vk`. Supabase custom providers are configured in **Authentication → Providers → New Provider**. Choose manual OAuth2 configuration, create the provider using the matching identifier, and register the callback URL displayed by Supabase in the Yandex/VK app settings. Provider client secrets stay in Supabase, never in Flutter. See [Supabase custom OAuth/OIDC setup](https://supabase.com/docs/guides/auth/custom-oauth-providers).

Yandex documents these OAuth URLs: authorization `https://oauth.yandex.ru/authorize`, token `https://oauth.yandex.ru/token`, profile `https://login.yandex.ru/info?format=json`. Yandex's profile API documents an `Authorization: OAuth` header; test the custom provider after setup because the generic OAuth2 user-info flow may not match this header convention. If it fails, it needs a server-side adapter rather than a secret in the browser.

For VK, create a VK ID web app and use the exact callback URL from the Supabase custom provider form. The button is wired to `custom:vk`, but the login still needs to be tested with the credentials and current web-app settings; if VK's flow does not fit the generic OAuth exchange, it needs a server-side adapter. Do not use older `oauth.vk.com` instructions unless VK's current developer console specifically offers that legacy flow.

## Realtime list

After the first profile query, the app subscribes to changes in `public.users`. A Realtime event triggers another read of only `username, avatar_url`, and the page redraws with the updated list. For this to work, the table must be in the `supabase_realtime` publication and RLS must grant the authenticated read policy from the SQL file.
