import 'package:flutter/material.dart';
import 'package:get/get.dart';

import '../../application/controllers/home_controller.dart';
import 'business_card.dart';

class RestaurantHomeTab extends StatelessWidget {
  const RestaurantHomeTab({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final HomeController controller = Get.find();

    return Obx(() {
      if (controller.isLoading.value) {
        return const Center(child: CircularProgressIndicator());
      }

      if (controller.businesses.isEmpty) {
        return Center(
          child: Padding(
            padding: const EdgeInsets.all(48),
            child: Text(
              'There are no businesses available at this moment. Please try '
              ' again another time.',
              textAlign: TextAlign.center,
              style: Theme.of(context).textTheme.titleMedium,
            ),
          ),
        );
      }

      return ListView.builder(
        padding: const EdgeInsets.all(8),
        itemCount: controller.businesses.length,
        itemBuilder: (context, index) {
          return BusinessCard(
            business: controller.businesses.elementAt(index),
            onPressed: controller.navigateToBusinessDetails,
            onFavorite: controller.favoriteBusiness,
          );
        },
      );
    });
  }
}
