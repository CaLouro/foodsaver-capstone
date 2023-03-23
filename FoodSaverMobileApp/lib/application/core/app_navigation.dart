import 'package:get/get.dart';

import '../../view/pages/business_info_page.dart';
import '../../view/pages/home_page.dart';
import '../../view/pages/login_page.dart';
import '../../view/pages/register_page.dart';

abstract class AppNavigation {
  static String get initialPath => '/login';

  static List<GetPage> get pages => [
        GetPage(name: '/login', page: () => const LoginPage()),
        GetPage(name: '/register', page: () => const RegisterPage()),
        GetPage(name: '/home', page: () => const HomePage()),
        GetPage(name: '/business', page: () => const BusinessInfoPage()),
      ];
}
