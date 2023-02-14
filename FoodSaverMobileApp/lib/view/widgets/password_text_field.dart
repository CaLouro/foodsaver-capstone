import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:form_builder_validators/form_builder_validators.dart';

import '../../application/validators/custom_validations.dart';
import 'visibility_icon_button.dart';

class PasswordTextField extends StatefulWidget {
  final String name;
  final InputDecoration decoration;

  const PasswordTextField(
    this.name, {
    Key? key,
    this.decoration = const InputDecoration(),
  }) : super(key: key);

  @override
  State<PasswordTextField> createState() => _PasswordTextFieldState();
}

class _PasswordTextFieldState extends State<PasswordTextField> {
  bool obscurePassword = true;

  void togglePasswordObfuscation() {
    setState(() {
      obscurePassword = !obscurePassword;
    });
  }

  @override
  Widget build(BuildContext context) {
    return FormBuilderTextField(
      name: widget.name,
      decoration: widget.decoration.copyWith(
        labelText: 'Password',
        suffixIcon: VisibilityIconButton(
          obscure: obscurePassword,
          onPressed: togglePasswordObfuscation,
        ),
        errorMaxLines: 2,
        helperMaxLines: 3,
      ),
      validator: FormBuilderValidators.compose([
        FormBuilderValidators.required(),
        FormBuilderValidators.minLength(CustomValidations.passwordMinLength),
        FormBuilderValidators.maxLength(CustomValidations.passwordMaxLength),
        CustomValidations.password(),
      ]),
      obscureText: obscurePassword,
      keyboardType: TextInputType.visiblePassword,
      autocorrect: false,
    );
  }
}
