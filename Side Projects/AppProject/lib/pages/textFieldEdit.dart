import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class Textfieldedit extends StatefulWidget {
  const Textfieldedit({
    super.key,
    required this.fullnamecontroller,
    required this.usernamecontroller,
    required this.emailcontroller,
    required this.passwordcontroller,
  });

  final TextEditingController fullnamecontroller;
  final TextEditingController usernamecontroller;
  final TextEditingController emailcontroller;
  final TextEditingController passwordcontroller;

  @override
  State<Textfieldedit> createState() => _TextfieldeditState();
}

class _TextfieldeditState extends State<Textfieldedit> {
  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        TextField(
          controller: widget.fullnamecontroller,
          obscureText: false,
          decoration: const InputDecoration(
            labelText: 'Full Name',
            border: OutlineInputBorder(),
            prefixIcon: Icon(Icons.person),
          ),
        ),
        const SizedBox(height: 16),
        TextField(
          controller: widget.usernamecontroller,
          obscureText: false,
          decoration: const InputDecoration(
            labelText: 'Username',
            border: OutlineInputBorder(),
            prefixIcon: Icon(Icons.account_circle),
          ),
        ),
        const SizedBox(height: 16),
        TextField(
          controller: widget.emailcontroller,
          obscureText: false,
          keyboardType: TextInputType.emailAddress,
          decoration: const InputDecoration(
            labelText: 'Email',
            border: OutlineInputBorder(),
            prefixIcon: Icon(Icons.email),
          ),
        ),
        const SizedBox(height: 16),
        TextField(
          controller: widget.passwordcontroller,
          obscureText: true,
          decoration: const InputDecoration(
            labelText: 'Password',
            border: OutlineInputBorder(),
            prefixIcon: Icon(Icons.lock),
          ),
        ),
      ],
    );
  }
}