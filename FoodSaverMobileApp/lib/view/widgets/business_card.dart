import 'package:flutter/material.dart';

import '../../domain/classes/business.dart';
import 'favorite_icon_button.dart';

typedef VoidBusinessCallback = void Function(Business);

class BusinessCard extends StatelessWidget {
  final Business business;
  final VoidBusinessCallback? onFavorite;
  final VoidBusinessCallback? onPressed;

  const BusinessCard({
    Key? key,
    required this.business,
    this.onFavorite,
    this.onPressed,
  }) : super(key: key);

  void onFavoriteIconPress(bool favoriteState) {
    business.isFavorite = favoriteState;
    onFavorite?.call(business);
  }

  VoidCallback? getOnTapBehaviour() {
    return onPressed == null ? null : () => onPressed?.call(business);
  }

  @override
  Widget build(BuildContext context) {
    return Card(
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8),
      ),
      child: InkWell(
        onTap: getOnTapBehaviour(),
        child: Padding(
          padding: const EdgeInsets.all(16),
          child: Row(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.stretch,
                  children: [
                    Padding(
                      padding: const EdgeInsets.only(bottom: 8),
                      child: Text(
                        business.name,
                        style: Theme.of(context).textTheme.titleLarge,
                        maxLines: 2,
                        overflow: TextOverflow.ellipsis,
                      ),
                    ),
                    Text(
                      business.address.presentAddress(),
                      style: Theme.of(context).textTheme.bodySmall,
                    ),
                  ],
                ),
              ),
              Padding(
                padding: const EdgeInsets.only(left: 8),
                child: FavoriteIconButton(
                  isFavorite: business.isFavorite,
                  onPressed: onFavoriteIconPress,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
