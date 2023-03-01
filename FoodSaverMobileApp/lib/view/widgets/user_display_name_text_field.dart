import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:form_builder_validators/form_builder_validators.dart';

class UserDisplayNameTextField extends StatelessWidget {
  final String name;
  final InputDecoration decoration;

  const UserDisplayNameTextField(
    this.name, {
    Key? key,
    this.decoration = const InputDecoration(),
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return FormBuilderTextField(
      name: name,
      decoration: decoration.copyWith(
        labelText: 'User Name',
        errorMaxLines: 2,
        helperMaxLines: 3,
      ),
      validator: FormBuilderValidators.required(
        errorText: 'Please provide an user name.',
      ),
      keyboardType: TextInputType.emailAddress,
      autocorrect: true,
    );
  }
}
