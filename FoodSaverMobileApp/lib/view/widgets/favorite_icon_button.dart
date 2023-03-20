import 'package:flutter/material.dart';

typedef VoidBoolCallback = void Function(bool);

class FavoriteIconButton extends StatefulWidget {
  const FavoriteIconButton({
    Key? key,
    required this.isFavorite,
    this.onPressed,
  }) : super(key: key);

  final bool isFavorite;
  final VoidBoolCallback? onPressed;

  @override
  State<FavoriteIconButton> createState() => _FavoriteIconButtonState();
}

class _FavoriteIconButtonState extends State<FavoriteIconButton> {
  late bool isFavorite;

  void toggleFavorite() {
    setState(() {
      isFavorite = !isFavorite;
    });
    widget.onPressed?.call(isFavorite);
  }

  @override
  void initState() {
    super.initState();
    isFavorite = widget.isFavorite;
  }

  @override
  Widget build(BuildContext context) {
    return IconButton(
      onPressed: toggleFavorite,
      icon: AnimatedSwitcher(
        duration: const Duration(milliseconds: 200),
        transitionBuilder: (child, animation) {
          return FadeTransition(
            opacity: animation,
            child: ScaleTransition(
              scale: animation,
              child: child,
            ),
          );
        },
        child: isFavorite
            ? const Icon(
                Icons.favorite_rounded,
                key: ValueKey(true),
                color: Colors.red,
              )
            : Icon(
                Icons.favorite_border_rounded,
                key: const ValueKey(false),
                color: Colors.grey.shade800,
              ),
      ),
    );
  }
}
