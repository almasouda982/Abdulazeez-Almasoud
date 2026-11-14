import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:universitymobileapp/App2/app/screens/homeScreen.dart';
import 'package:universitymobileapp/App2/app/screens/loginScreen.dart';

import 'App2/providers/BookProvider.dart';
import 'App2/providers/CourseBookProvider.dart';
import 'App2/providers/CoursePrerequisiteProvider.dart';
import 'App2/providers/CourseProvider.dart';
import 'App2/providers/CourseSectionProvider.dart';
import 'App2/providers/DegreeProgramProvider.dart';
import 'App2/providers/DegreeRequirementProvider.dart';
import 'App2/providers/EnrollmentProvider.dart';
import 'App2/providers/StudentProvider.dart';
import 'App2/providers/UserProvider.dart';
import 'App2/providers/WaitlistEntryProvider.dart';

// void main() {
//   runApp(const MyApp());
// }


void main() {
  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(
          create: (context) => UserProvider(),
        ),

        ChangeNotifierProvider(
          create: (context) => BookProvider(),
        ),

        ChangeNotifierProvider(
          create: (context) => CourseProvider(),
        ),

        ChangeNotifierProvider(
          create: (context) => CourseBookProvider(),
        ),

        ChangeNotifierProvider(
          create: (context) => CoursePrerequisiteProvider(),
        ),

        ChangeNotifierProvider(
          create: (context) => CourseSectionProvider(),
        ),

        ChangeNotifierProvider(
          create: (context) => DegreeProgramProvider(),
        ),

        ChangeNotifierProvider(
          create: (context) => DegreeRequirementProvider(),
        ),

        ChangeNotifierProvider(
          create: (context) => EnrollmentProvider(),
        ),

        ChangeNotifierProvider(
          create: (context) => StudentProvider(),
        ),

        ChangeNotifierProvider(
          create: (context) => WaitlistEntryProvider(),
        ),

      ],
      child: const MyApp(),
    ),
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

