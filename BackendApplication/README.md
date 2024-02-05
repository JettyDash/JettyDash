
---
### Pfx File Creation

To create a pfx file from the generated key and certificate files, run the following command in the terminal.
Requirements: openssl, mkcert \

TODO: take cert from vault
```bash
cd BackendApplication

openssl pkcs12 -export -out certificate.pfx -inkey localhost-key.pem -in localhost.pem

```