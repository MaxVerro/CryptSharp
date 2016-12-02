# CryptSharp

This repository contains a simple library to hash passwords in C#. So far, passwords can only be hashed with PBKDF2 using a 
cryptographically random salt. The goal is to provide a simple library,  easily reconfigured at runtime 
(ie: change the hashing algorithm) without having to rebuild source code while maintaining a backward compatibility for older password.



