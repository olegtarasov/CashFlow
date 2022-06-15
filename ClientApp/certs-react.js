// This script configures the .env.development.local file with additional environment variables to configure HTTPS using the ASP.NET Core
// development certificate in the webpack development proxy.

const fs = require('fs');
const path = require('path');

const https = process.env.HTTPS !== undefined && (process.env.HTTPS === "" || process.env.HTTPS.toLowerCase().trim() === "true");

if (!https) {
  console.info("Not using HTTPS")
  process.exit();
}

const baseFolder = `${process.cwd()}/../certs`;
const certFilePath = path.join(baseFolder, "localhost.crt");
const keyFilePath = path.join(baseFolder, "localhost.key");

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
  // When there are no certs, assume we don't use HTTPS.
  throw new Error("HTTPS environment variable is true, but certificates do not exist")
}

if (!fs.existsSync(".env.development.local")) {
  fs.writeFileSync(".env.development.local",
`SSL_CRT_FILE=${certFilePath}
SSL_KEY_FILE=${keyFilePath}`
  );
} else {
  let lines = fs.readFileSync('.env.development.local').toString().split('\n');
  let hasCert, hasCertKey = false;
  for (const line of lines) {
    if (/SSL_CRT_FILE=.*/i.test(line)) {
      hasCert = true;
    }
    if (/SSL_KEY_FILE=.*/i.test(line)) {
      hasCertKey = true;
    }
  }
  if (!hasCert) {
    fs.appendFileSync(".env.development.local", `\nSSL_CRT_FILE=${certFilePath}`);
  }
  if (!hasCertKey) {
    fs.appendFileSync(".env.development.local", `\nSSL_KEY_FILE=${keyFilePath}`);
  }
}
