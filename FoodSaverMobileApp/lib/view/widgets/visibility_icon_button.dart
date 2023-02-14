import 'package:flutter/material.dart';

class VisibilityIconButton extends StatelessWidget {
  const VisibilityIconButton({
    Key? key,
    required this.obscure,
    this.onPressed,
  }) : super(key: key);

  final bool obscure;
  final VoidCallback? onPressed;

  @override
  Widget build(BuildContext context) {
    return IconButton(
      onPressed: onPressed,
      icon: Icon(
        obscure ? Icons.visibility_off : Icons.visibility,
      ),
    );
  }
}
