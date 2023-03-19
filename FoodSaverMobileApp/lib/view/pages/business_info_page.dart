import 'package:flutter/material.dart';
import 'package:get/get.dart';

import '../../application/controllers/business_info_controller.dart';
import '../widgets/favorite_icon_button.dart';

class BusinessInfoPage extends StatefulWidget {
  const BusinessInfoPage({Key? key}) : super(key: key);

  @override
  State<BusinessInfoPage> createState() => _BusinessInfoPageState();
}

class _BusinessInfoPageState extends State<BusinessInfoPage> {
  final controller = Get.put(BusinessInfoController());

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // appBar: AppBar(
      //   title: const Text('Business'),
      // ),
      // body: ListView(
      //   children: [
      //     Container(
      //       padding: const EdgeInsets.all(24),
      //       color: Colors.blueGrey,
      //       child: Column(
      //         mainAxisSize: MainAxisSize.min,
      //         crossAxisAlignment: CrossAxisAlignment.stretch,
      //         children: [
      //           Text(
      //             controller.businessName,
      //             textAlign: TextAlign.center,
      //             style: Theme.of(context).textTheme.titleLarge,
      //           ),
      //         ],
      //       ),
      //     )
      //   ],
      // ),
      body: CustomScrollView(
        slivers: [
          SliverAppBar(
            actions: [
              FavoriteIconButton(
                isFavorite: true,
                onPressed: controller.onFavoriteIconPress,
              )
            ],
            pinned: true,
            snap: true,
            floating: true,
            expandedHeight: 150,
            title: Text(controller.businessName),
            centerTitle: true,
            flexibleSpace: FlexibleSpaceBar(
              background: DefaultTextStyle(
                style: Theme.of(context)
                        .textTheme
                        .bodyLarge
                        ?.copyWith(color: Colors.grey.shade800) ??
                    const TextStyle(),
                child: Container(
                  padding: const EdgeInsets.all(16),
                  alignment: Alignment.bottomCenter,
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      Text(
                        controller.businessAddress,
                      ),
                      Row(
                        children: [
                          Expanded(
                            child: Text(
                              controller.businessEmailContact,
                              textAlign: TextAlign.right,
                            ),
                          ),
                          const Text(
                            ' • ',
                          ),
                          Expanded(
                            child: Text(
                              controller.businessPhoneContact,
                              textAlign: TextAlign.left,
                            ),
                          ),
                        ],
                      ),
                    ],
                  ),
                ),
              ),
            ),
          ),
          SliverList(
            // Use a delegate to build items as they're scrolled on screen.
            delegate: SliverChildBuilderDelegate(
              // The builder function returns a ListTile with a title that
              // displays the index of the current item.
              (context, index) => ListTile(title: Text('Item #$index')),
              // Builds 1000 ListTiles
              childCount: 1000,
            ),
          )
        ],
      ),
    );
  }
}
