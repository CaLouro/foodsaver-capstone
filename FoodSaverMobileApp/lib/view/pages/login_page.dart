import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:get/get.dart';

import '../../application/controllers/login_controller.dart';
import '../widgets/email_text_field.dart';
import '../widgets/password_text_field.dart';
import '../widgets/styled_rounded_loading_button.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({Key? key}) : super(key: key);

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  LoginController controller = Get.put(LoginController());

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Food Saver'),
        centerTitle: true,
      ),
      body: FormBuilder(
        key: controller.formKey,
        clearValueOnUnregister: false,
        child: ListView(
          padding: const EdgeInsets.all(16),
          children: [
            Padding(
              padding: const EdgeInsets.symmetric(vertical: 16),
              child: Text(
                'Login',
                style: Theme.of(context).textTheme.headlineSmall,
                textAlign: TextAlign.center,
              ),
            ),
            const EmailTextField('email'),
            const PasswordTextField('password'),
            Padding(
              padding: const EdgeInsets.symmetric(vertical: 16),
              child: Row(
                children: [
                  Expanded(
                    flex: 4,
                    child: TextButton(
                      onPressed: controller.goToSignUp,
                      child: const Text('Sign Up'),
                    ),
                  ),
                  const Spacer(flex: 1),
                  Expanded(
                    flex: 7,
                    child: StyledRoundedLoadingButton(
                      controller: controller.buttonController,
                      onPressed: controller.submitLogin,
                      child: const Text('Login'),
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
