import 'package:get/get.dart';

class ErrorSnackbar {
  final String message;

  const ErrorSnackbar(this.message);

  show() {
    Get.snackbar(
      'Error',
      message,
      snackPosition: SnackPosition.BOTTOM,
    );
  }
}
