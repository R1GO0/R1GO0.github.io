import 'dart:async';

import 'package:flutter/material.dart';
import 'package:supabase_flutter/supabase_flutter.dart';

const _ink = Color(0xFF181923);
const _muted = Color(0xFF777987);
const _purple = Color(0xFF6855E8);
const _page = Color(0xFFF7F7FB);

class AppConfig {
  static const supabaseUrl = String.fromEnvironment(
    'SUPABASE_URL',
    defaultValue: 'https://iohyswibycvddexmtcwv.supabase.co',
  );
  static const publishableKey = String.fromEnvironment(
    'SUPABASE_PUBLISHABLE_KEY',
  );
  static const legacyAnonKey = String.fromEnvironment('SUPABASE_ANON_KEY');
  static String get apiKey =>
      publishableKey.isNotEmpty ? publishableKey : legacyAnonKey;

  static bool get isConfigured => supabaseUrl.isNotEmpty && apiKey.isNotEmpty;
}

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  var supabaseReady = false;

  if (AppConfig.isConfigured) {
    try {
      await Supabase.initialize(
        url: AppConfig.supabaseUrl,
        publishableKey: AppConfig.apiKey,
      );
      supabaseReady = true;
    } catch (error) {
      debugPrint('Supabase initialization failed: $error');
    }
  }

  runApp(LiveboardApp(supabaseReady: supabaseReady));
}

class LiveboardApp extends StatelessWidget {
  const LiveboardApp({super.key, required this.supabaseReady});

  final bool supabaseReady;

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Liveboard',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        useMaterial3: true,
        scaffoldBackgroundColor: _page,
        colorScheme: ColorScheme.fromSeed(seedColor: _purple),
        fontFamily: 'Arial',
      ),
      home: AppShell(supabaseReady: supabaseReady),
    );
  }
}

class AppShell extends StatefulWidget {
  const AppShell({super.key, required this.supabaseReady});

  final bool supabaseReady;

  @override
  State<AppShell> createState() => _AppShellState();
}

class _AppShellState extends State<AppShell> {
  StreamSubscription<AuthState>? _authSubscription;

  User? get _user =>
      widget.supabaseReady ? Supabase.instance.client.auth.currentUser : null;

  @override
  void initState() {
    super.initState();
    if (widget.supabaseReady) {
      _authSubscription = Supabase.instance.client.auth.onAuthStateChange
          .listen((_) {
            if (mounted) setState(() {});
          });
    }
  }

  @override
  void dispose() {
    _authSubscription?.cancel();
    super.dispose();
  }

  Future<void> _signIn(String providerId) async {
    if (!widget.supabaseReady) return;

    final provider = switch (providerId) {
      'github' => OAuthProvider.github,
      'google' => OAuthProvider.google,
      'apple' => OAuthProvider.apple,
      'yandex' => const OAuthProvider('custom:yandex'),
      'vk' => const OAuthProvider('custom:vk'),
      _ => throw ArgumentError.value(providerId, 'providerId'),
    };

    try {
      await Supabase.instance.client.auth.signInWithOAuth(
        provider,
        redirectTo: Uri.base.replace(query: null, fragment: null).toString(),
      );
    } catch (error) {
      _showMessage(_authError(error));
    }
  }

  Future<void> _signOut() async {
    try {
      await Supabase.instance.client.auth.signOut();
    } catch (error) {
      _showMessage('Не удалось выйти: $error');
    }
  }

  void _showMessage(String message) {
    if (!mounted) return;
    ScaffoldMessenger.of(context)
      ..hideCurrentSnackBar()
      ..showSnackBar(SnackBar(content: Text(message)));
  }

  String _authError(Object error) {
    if (error is AuthException) return error.message;
    return 'Не получилось запустить вход. Проверьте настройки провайдера в Supabase.';
  }

  @override
  Widget build(BuildContext context) {
    final user = _user;
    if (user == null) {
      return SignInPage(supabaseReady: widget.supabaseReady, onSignIn: _signIn);
    }
    return DashboardPage(user: user, onSignOut: _signOut);
  }
}

class SignInPage extends StatelessWidget {
  const SignInPage({
    super.key,
    required this.supabaseReady,
    required this.onSignIn,
  });

  final bool supabaseReady;
  final Future<void> Function(String providerId) onSignIn;

  static const _providers = <_ProviderInfo>[
    _ProviderInfo('google', 'Google / Gmail', 'G', Color(0xFF4285F4)),
    _ProviderInfo('github', 'GitHub', 'GH', Color(0xFF252631)),
    _ProviderInfo('apple', 'Apple', 'A', Color(0xFF252631)),
    _ProviderInfo('yandex', 'Яндекс', 'Я', Color(0xFFFF4545)),
    _ProviderInfo('vk', 'ВКонтакте', 'VK', Color(0xFF2787F5)),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Center(
          child: SingleChildScrollView(
            padding: const EdgeInsets.all(20),
            child: ConstrainedBox(
              constraints: const BoxConstraints(maxWidth: 470),
              child: _SignInCard(
                supabaseReady: supabaseReady,
                onSignIn: onSignIn,
                providers: _providers,
              ),
            ),
          ),
        ),
      ),
    );
  }
}

class _SignInCard extends StatelessWidget {
  const _SignInCard({
    required this.supabaseReady,
    required this.onSignIn,
    required this.providers,
  });

  final bool supabaseReady;
  final Future<void> Function(String providerId) onSignIn;
  final List<_ProviderInfo> providers;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.all(30),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(28),
        border: Border.all(color: const Color(0xFFEAEAF1)),
        boxShadow: const [
          BoxShadow(
            color: Color(0x090D1020),
            blurRadius: 30,
            offset: Offset(0, 12),
          ),
        ],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            'С возвращением',
            style: TextStyle(
              color: _ink,
              fontSize: 27,
              fontWeight: FontWeight.w800,
              letterSpacing: -.8,
            ),
          ),
          const SizedBox(height: 7),
          const Text(
            'Выберите удобный способ входа',
            style: TextStyle(color: _muted, fontSize: 14),
          ),
          const SizedBox(height: 25),
          for (final provider in providers) ...[
            _ProviderButton(
              provider: provider,
              onPressed: () {
                if (!supabaseReady) {
                  ScaffoldMessenger.of(context)
                    ..hideCurrentSnackBar()
                    ..showSnackBar(
                      const SnackBar(
                        content: Text(
                          'Добавьте publishable key проекта Supabase. Инструкция — в README.',
                        ),
                      ),
                    );
                  return;
                }
                onSignIn(provider.id);
              },
            ),
            const SizedBox(height: 10),
          ],
        ],
      ),
    );
  }
}

class _ProviderButton extends StatelessWidget {
  const _ProviderButton({required this.provider, required this.onPressed});

  final _ProviderInfo provider;
  final VoidCallback onPressed;

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: double.infinity,
      height: 49,
      child: OutlinedButton(
        onPressed: onPressed,
        style: OutlinedButton.styleFrom(
          foregroundColor: _ink,
          backgroundColor: Colors.white,
          side: const BorderSide(color: Color(0xFFE7E8EF)),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(14),
          ),
          padding: const EdgeInsets.symmetric(horizontal: 15),
        ),
        child: Row(
          children: [
            Container(
              width: 27,
              height: 27,
              alignment: Alignment.center,
              decoration: BoxDecoration(
                color: provider.color.withValues(alpha: .09),
                borderRadius: BorderRadius.circular(9),
              ),
              child: Text(
                provider.mark,
                style: TextStyle(
                  color: provider.color,
                  fontSize: provider.mark.length > 1 ? 10 : 15,
                  fontWeight: FontWeight.w800,
                ),
              ),
            ),
            const SizedBox(width: 12),
            Expanded(
              child: Text(
                'Продолжить через ${provider.label}',
                style: const TextStyle(
                  fontSize: 13,
                  fontWeight: FontWeight.w700,
                ),
              ),
            ),
            const Icon(
              Icons.arrow_forward_rounded,
              size: 17,
              color: Color(0xFF9A9BA6),
            ),
          ],
        ),
      ),
    );
  }
}

class _ProviderInfo {
  const _ProviderInfo(this.id, this.label, this.mark, this.color);

  final String id;
  final String label;
  final String mark;
  final Color color;
}

class DashboardPage extends StatefulWidget {
  const DashboardPage({super.key, required this.user, required this.onSignOut});

  final User user;
  final Future<void> Function() onSignOut;

  @override
  State<DashboardPage> createState() => _DashboardPageState();
}

class _DashboardPageState extends State<DashboardPage> {
  final List<Map<String, dynamic>> _profiles = [];
  RealtimeChannel? _profilesChannel;
  bool _loadingProfiles = true;
  bool _fetchingProfiles = false;
  bool _reloadAfterFetch = false;
  String? _profilesError;

  @override
  void initState() {
    super.initState();
    unawaited(_loadProfiles());
    _profilesChannel = Supabase.instance.client
        .channel('public:users:${widget.user.id}')
        .onPostgresChanges(
          event: PostgresChangeEvent.all,
          schema: 'public',
          table: 'users',
          select: const ['username', 'avatar_url'],
          callback: (_) => unawaited(_loadProfiles()),
        )
        .subscribe();
  }

  Future<void> _loadProfiles() async {
    if (!mounted) return;
    if (_fetchingProfiles) {
      _reloadAfterFetch = true;
      return;
    }
    _fetchingProfiles = true;

    try {
      final rows = await Supabase.instance.client
          .from('users')
          .select('username, avatar_url')
          .order('username');
      if (!mounted) return;
      setState(() {
        _profiles
          ..clear()
          ..addAll(List<Map<String, dynamic>>.from(rows));
        _profilesError = null;
        _loadingProfiles = false;
      });
    } catch (_) {
      if (!mounted) return;
      setState(() {
        _profilesError =
            'Проверьте доступ к public.users и выполните SQL из SUPABASE_SETUP.sql.';
        _loadingProfiles = false;
      });
    } finally {
      _fetchingProfiles = false;
      if (_reloadAfterFetch && mounted) {
        _reloadAfterFetch = false;
        unawaited(_loadProfiles());
      }
    }
  }

  @override
  void dispose() {
    final channel = _profilesChannel;
    if (channel != null) {
      unawaited(Supabase.instance.client.removeChannel(channel));
    }
    super.dispose();
  }

  String get displayName {
    final metadata = widget.user.userMetadata ?? const <String, dynamic>{};
    for (final key in [
      'full_name',
      'name',
      'user_name',
      'preferred_username',
    ]) {
      final value = metadata[key];
      if (value is String && value.trim().isNotEmpty) return value.trim();
    }
    final email = widget.user.email;
    if (email != null && email.contains('@')) return email.split('@').first;
    return email ?? 'Пользователь';
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Center(
          child: ConstrainedBox(
            constraints: const BoxConstraints(maxWidth: 1120),
            child: SingleChildScrollView(
              padding: const EdgeInsets.symmetric(horizontal: 22, vertical: 24),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  _DashboardHeader(
                    displayName: displayName,
                    onSignOut: widget.onSignOut,
                  ),
                  const SizedBox(height: 24),
                  _ProfilesCard(
                    profiles: _profiles,
                    isLoading: _loadingProfiles,
                    error: _profilesError,
                    onRetry: _loadProfiles,
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}

class _DashboardHeader extends StatelessWidget {
  const _DashboardHeader({required this.displayName, required this.onSignOut});

  final String displayName;
  final Future<void> Function() onSignOut;

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        Expanded(
          child: Text(
            displayName,
            style: const TextStyle(
              color: _ink,
              fontSize: 26,
              fontWeight: FontWeight.w800,
              letterSpacing: -.7,
            ),
          ),
        ),
        OutlinedButton.icon(
          onPressed: onSignOut,
          icon: const Icon(Icons.logout_rounded, size: 16),
          label: const Text('Выйти'),
          style: OutlinedButton.styleFrom(
            foregroundColor: const Color(0xFF555765),
            side: const BorderSide(color: Color(0xFFE3E4EB)),
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(12),
            ),
            textStyle: const TextStyle(
              fontSize: 12,
              fontWeight: FontWeight.w700,
            ),
          ),
        ),
      ],
    );
  }
}

class _ProfilesCard extends StatelessWidget {
  const _ProfilesCard({
    required this.profiles,
    required this.isLoading,
    required this.error,
    required this.onRetry,
  });

  final List<Map<String, dynamic>> profiles;
  final bool isLoading;
  final String? error;
  final Future<void> Function() onRetry;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.all(22),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(20),
        border: Border.all(color: const Color(0xFFEAEAF1)),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            'Список',
            style: TextStyle(
              color: _ink,
              fontSize: 16,
              fontWeight: FontWeight.w800,
            ),
          ),
          const SizedBox(height: 17),
          if (error != null)
            _EmptyOrError(
              icon: Icons.cloud_off_rounded,
              title: 'Не удалось загрузить список',
              body: error,
            )
          else if (isLoading && profiles.isEmpty)
            const Padding(
              padding: EdgeInsets.symmetric(vertical: 32),
              child: Center(child: CircularProgressIndicator()),
            )
          else if (profiles.isEmpty)
            const _EmptyOrError(icon: Icons.inbox_rounded, title: 'Список пуст')
          else
            ...profiles.map((profile) => _ProfileRow(profile: profile)),
          if (error != null) ...[
            const SizedBox(height: 12),
            Align(
              alignment: Alignment.centerRight,
              child: TextButton.icon(
                onPressed: onRetry,
                icon: const Icon(Icons.refresh_rounded, size: 16),
                label: const Text('Повторить'),
              ),
            ),
          ],
        ],
      ),
    );
  }
}

class _EmptyOrError extends StatelessWidget {
  const _EmptyOrError({required this.icon, required this.title, this.body});

  final IconData icon;
  final String title;
  final String? body;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.symmetric(horizontal: 18, vertical: 31),
      decoration: BoxDecoration(
        color: const Color(0xFFFAFAFC),
        borderRadius: BorderRadius.circular(15),
      ),
      child: Column(
        children: [
          Icon(icon, color: const Color(0xFFA3A4AF), size: 27),
          const SizedBox(height: 10),
          Text(
            title,
            style: const TextStyle(
              color: _ink,
              fontSize: 13,
              fontWeight: FontWeight.w700,
            ),
          ),
          if (body != null && body!.isNotEmpty) ...[
            const SizedBox(height: 5),
            Text(
              body!,
              textAlign: TextAlign.center,
              style: const TextStyle(color: _muted, fontSize: 12, height: 1.45),
            ),
          ],
        ],
      ),
    );
  }
}

class _ProfileRow extends StatelessWidget {
  const _ProfileRow({required this.profile});

  final Map<String, dynamic> profile;

  @override
  Widget build(BuildContext context) {
    final username = profile['username']?.toString() ?? 'Без имени';
    final avatarUrl = profile['avatar_url']?.toString();

    return Padding(
      padding: const EdgeInsets.only(bottom: 10),
      child: Container(
        padding: const EdgeInsets.all(14),
        decoration: BoxDecoration(
          border: Border.all(color: const Color(0xFFEDEDF2)),
          borderRadius: BorderRadius.circular(14),
        ),
        child: Row(
          children: [
            CircleAvatar(
              radius: 18,
              backgroundColor: const Color(0xFFF3F1FF),
              backgroundImage: avatarUrl != null && avatarUrl.isNotEmpty
                  ? NetworkImage(avatarUrl)
                  : null,
              child: avatarUrl == null || avatarUrl.isEmpty
                  ? const Icon(Icons.person_rounded, color: _purple, size: 19)
                  : null,
            ),
            const SizedBox(width: 12),
            Expanded(
              child: Text(
                username,
                style: const TextStyle(
                  color: _ink,
                  fontSize: 13,
                  fontWeight: FontWeight.w600,
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
