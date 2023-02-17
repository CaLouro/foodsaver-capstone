import 'package:firebase_auth/firebase_auth.dart';
import 'package:get/get.dart';

import '../domain/exceptions/auth_exception.dart';
import '../domain/services/auth_service_interface.dart';

class AuthService extends GetxService implements IAuthService {
  FirebaseAuth get _auth => FirebaseAuth.instance;

  @override
  bool isLoggedIn() => _auth.currentUser != null;

  @override
  String getUserUid() {
    final userUid = _auth.currentUser?.uid;
    if (userUid == null) {
      throw UserUnauthenticatedException();
    }

    return userUid;
  }

  @override
  String getUserDisplayName() {
    final displayName = _auth.currentUser?.displayName;
    if (displayName == null) {
      throw UserUnauthenticatedException();
    }

    return displayName;
  }

  @override
  Future<void> signOut() async {
    await _auth.signOut();
  }

  @override
  Future<void> anonymousSignIn() async {
    try {
      await _auth.signInAnonymously();
    } on FirebaseAuthException catch (exc) {
      throw AuthException(exc);
    }
  }

  @override
  Future<void> signUp(String email, String password, {String? username}) async {
    try {
      final userCredentials = await _auth.createUserWithEmailAndPassword(
        email: email,
        password: password,
      );

      if (username != null) {
        await userCredentials.user!.updateDisplayName(username);
      }
    } on FirebaseAuthException catch (exc) {
      throw AuthException(exc);
    }
  }

  @override
  Future<void> signIn(String email, String password) async {
    try {
      await _auth.signInWithEmailAndPassword(
        email: email,
        password: password,
      );
    } on FirebaseAuthException catch (exc) {
      throw AuthException(exc);
    }
  }
}
