import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:universitymobileapp/App2/app/screens/homeScreen.dart';
import 'package:universitymobileapp/App2/app/screens/loginScreen.dart';

import 'App2/providers/UserProvider.dart';

// void main() {
//   runApp(const MyApp());
// }


void main() {
  runApp(
      MultiProvider(
        providers: [
          ChangeNotifierProvider(create: (_) => UserProvider()),
        ],
        child: const MyApp(),
      )
  );
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        colorScheme: .fromSeed(seedColor: Colors.deepPurple),
      ),
      home: LoginScreen(),
      debugShowCheckedModeBanner: false,
    );
  }
}

