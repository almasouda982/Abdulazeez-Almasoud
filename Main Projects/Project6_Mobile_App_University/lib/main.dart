import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:universitymobileapp/App1/app/screens/enrollmentScreen.dart';
import 'package:universitymobileapp/App1/app/screens/loginScreen.dart';

// void main() {
//   runApp(const MyApp());
// }


void main() {
  runApp(
      MultiProvider(
        providers: [

        ],
        child:   const MyApp(),
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

