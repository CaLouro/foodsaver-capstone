import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:form_builder_validators/form_builder_validators.dart';

import '../../application/validators/custom_validations.dart';
import 'visibility_icon_button.dart';

class PasswordMatchTextField extends StatefulWidget {
  final String name;
  final String fieldNameToMatch;
  final InputDecoration inputDecoration;
  final String? passwordMatchErrorText;

  const PasswordMatchTextField(
    this.name, {
    Key? key,
    required this.fieldNameToMatch,
    this.inputDecoration = const InputDecoration(),
    this.passwordMatchErrorText,
  }) : super(key: key);

  @override
  State<PasswordMatchTextField> createState() => _PasswordMatchTextFieldState();
}

class _PasswordMatchTextFieldState extends State<PasswordMatchTextField> {
  bool obscurePassword = true;

  void togglePasswordObfuscation() {
    setState(() {
      obscurePassword = !obscurePassword;
    });
  }

  String? getPasswordValue() {
    return FormBuilder.of(context)?.instantValue[widget.fieldNameToMatch];
  }

  @override
  Widget build(BuildContext context) {
    return FormBuilderTextField(
      name: widget.name,
      decoration: widget.inputDecoration.copyWith(
        labelText: 'Confirm Password',
        suffixIcon: VisibilityIconButton(
          obscure: obscurePassword,
          onPressed: togglePasswordObfuscation,
        ),
      ),
      validator: FormBuilderValidators.compose([
        FormBuilderValidators.required(),
        CustomValidations.passwordMatch(
          getPasswordValue,
          errorText: widget.passwordMatchErrorText,
        ),
      ]),
      obscureText: obscurePassword,
      keyboardType: TextInputType.visiblePassword,
      autocorrect: false,
    );
  }
}
