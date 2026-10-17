

// صفحة Home
import 'package:flutter/material.dart';

class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Home Screen'),
        backgroundColor: Colors.green,
      ),
      body: const Center(
        child: const Text(
          'Welcome Home 👋',
          style: const TextStyle(fontSize: 22),
        ),
      ),
    );
  }
}
