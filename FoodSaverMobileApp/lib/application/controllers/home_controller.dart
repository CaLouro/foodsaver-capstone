import 'package:get/get.dart';

import '../../domain/exceptions/auth_exception.dart';
import '../../domain/services/auth_service_interface.dart';
import '../../view/utils/error_snackbar.dart';
import '../core/app_messages.dart';

class HomeController extends GetxController {
  final RxInt currentTabIndex = RxInt(0);

  void changeCurrentTabIndex(int value) {
    currentTabIndex.value = value;
  }

  Future<void> signOut() async {
    try {
      await Get.find<IAuthService>().signOut();
      Get.offAllNamed('/login');
    } on AuthException catch (e) {
      ErrorSnackbar(AppMessages.localizeAuthExceptionMessage(e)).show();
    } catch (_) {
      const ErrorSnackbar(AppMessages.genericErrorMessage).show();
    }
  }
}
