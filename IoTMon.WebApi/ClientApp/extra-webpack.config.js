const path = require('path');
const http = path.resolve(__dirname, 'node_modules/stream-http/index.js')

module.exports = {
  node: {
    crypto: true,
    http: true,
    https: true,
    os: true,
    vm: true,
    stream: true
  },
  resolve: {
    alias: { http, https: http }
  }

}