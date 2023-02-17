import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:form_builder_validators/form_builder_validators.dart';

class EmailTextField extends StatelessWidget {
  final String name;
  final InputDecoration decoration;

  const EmailTextField(
    this.name, {
    Key? key,
    this.decoration = const InputDecoration(),
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return FormBuilderTextField(
      name: name,
      decoration: decoration.copyWith(
        labelText: 'Email',
        errorMaxLines: 2,
        helperMaxLines: 3,
      ),
      validator: FormBuilderValidators.compose([
        FormBuilderValidators.required(
          errorText: 'Please provide an email.',
        ),
        FormBuilderValidators.email(
          errorText: 'Value is not in a valid email format',
        ),
      ]),
      keyboardType: TextInputType.emailAddress,
      autocorrect: true,
    );
  }
}
