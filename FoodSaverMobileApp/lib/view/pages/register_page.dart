import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:get/get.dart';

import '../../application/controllers/register_controller.dart';
import '../widgets/email_text_field.dart';
import '../widgets/password_match_text_field.dart';
import '../widgets/password_text_field.dart';
import '../widgets/styled_rounded_loading_button.dart';

class RegisterPage extends StatefulWidget {
  const RegisterPage({Key? key}) : super(key: key);

  @override
  State<RegisterPage> createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> {
  RegisterController controller = Get.put(RegisterController());

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Food Saver'),
        centerTitle: true,
      ),
      body: FormBuilder(
        key: controller.formKey,
        child: ListView(
          padding: const EdgeInsets.all(16),
          children: [
            Padding(
              padding: const EdgeInsets.symmetric(vertical: 16),
              child: Text(
                'Sign Up',
                style: Theme.of(context).textTheme.headlineSmall,
                textAlign: TextAlign.center,
              ),
            ),
            const EmailTextField(
              'email',
              decoration: InputDecoration(
                helperText: 'The email that will be used for signing in.',
              ),
            ),
            const PasswordTextField(
              'password',
              decoration: InputDecoration(
                helperText: 'The password must be at least 6 characters long '
                    'and contain at least one upper case character, one lower '
                    'case, and a number.',
              ),
            ),
            const PasswordMatchTextField(
              'confirm-password',
              fieldNameToMatch: 'password',
              passwordMatchErrorText: 'Passwords do not match.',
            ),
            Padding(
              padding: const EdgeInsets.symmetric(vertical: 16),
              child: StyledRoundedLoadingButton(
                controller: controller.buttonController,
                onPressed: controller.submitSignUp,
                child: const Text('Sign Up'),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
