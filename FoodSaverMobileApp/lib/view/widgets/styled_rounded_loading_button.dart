import 'package:flutter/material.dart';
import 'package:rounded_loading_button/rounded_loading_button.dart';

class StyledRoundedLoadingButton extends StatelessWidget {
  final RoundedLoadingButtonController controller;
  final VoidCallback onPressed;
  final Widget child;

  const StyledRoundedLoadingButton({
    Key? key,
    required this.controller,
    required this.onPressed,
    required this.child,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(builder: (context, constraints) {
      return RoundedLoadingButton(
        color: Theme.of(context).primaryColor,
        borderRadius: 4,
        height: 36,
        width: constraints.maxWidth,
        controller: controller,
        onPressed: onPressed,
        child: child,
      );
    });
  }
}
