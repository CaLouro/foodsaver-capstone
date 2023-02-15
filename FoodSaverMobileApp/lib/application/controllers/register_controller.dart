import 'dart:async';

import 'package:flutter/widgets.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:get/get.dart';
import 'package:rounded_loading_button/rounded_loading_button.dart';

import '../../domain/services/auth_service_interface.dart';

class RegisterController extends GetxController {
  final GlobalKey<FormBuilderState> formKey = GlobalKey();

  final RoundedLoadingButtonController buttonController =
      RoundedLoadingButtonController();

  Future<void> submitSignUp() async {
    assert(formKey.currentState != null);

    if (!formKey.currentState!.saveAndValidate()) {
      _showButtonError();
      return;
    }

    try {
      await _commitSignUp(formKey.currentState!.value);
      _showButtonSuccessAndNavigate();
    } on Exception catch (_) {
      _showButtonError();
      Get.snackbar(
        'Error',
        'Something went wrong. Please try again',
        snackPosition: SnackPosition.BOTTOM,
      );
    }
  }

  Future<void> _commitSignUp(Map<String, dynamic> values) {
    final email = values['email'];
    final password = values['password'];

    return Get.find<IAuthService>().signUp(email, password);
  }

  void _showButtonError() {
    buttonController.error();
    Timer(const Duration(seconds: 1), () => buttonController.reset());
  }

  void _showButtonSuccessAndNavigate() {
    buttonController.success();
    Timer(const Duration(seconds: 1), () => Get.offAllNamed('/home'));
  }
}
