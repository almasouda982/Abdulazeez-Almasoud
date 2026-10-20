import 'package:flutter/material.dart';
import 'MyTextFormField.dart';
import 'loginScreen.dart';

class RegisterScreen extends StatelessWidget {
  const RegisterScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.blue,
        title: const Text('Register'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(12.0),
        child: Form(
          child: Column(
            children: [
              MyTextFormField(name: 'Username'),
              const SizedBox(
                height: 20,
              ),
              MyTextFormField(name: 'Email'),
              const SizedBox(
                height: 20,
              ),
              MyTextFormField(name: 'Password'),
              const SizedBox(
                height: 20,
              ),
              ElevatedButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => const LoginScreen(),
                    ),
                  );
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.blue,
                  minimumSize: const Size.fromHeight(50),
                ),
                child: const Text(
                  'Register',
                  style: TextStyle(color: Colors.white, fontSize: 18),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}