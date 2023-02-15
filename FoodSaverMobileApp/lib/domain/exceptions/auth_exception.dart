import 'package:firebase_auth/firebase_auth.dart';

class AuthException implements Exception {
  const AuthException(this._source);

  final FirebaseAuthException _source;

  String? get message => _source.message;
  String get code => _source.code;
}

class UserUnauthenticatedException implements Exception {}
