import 'package:get/get.dart';

import '../../services/auth_service.dart';
import '../services/auth_service_interface.dart';

class AppBindings extends Bindings {
  @override
  void dependencies() {
    Get.put<IAuthService>(AuthService());
  }
}
