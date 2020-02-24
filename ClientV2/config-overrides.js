// config-overrides.js

const {
    override,
    fixBabelImports,
    addWebpackAlias,
   /* addLessLoader,*/
} = require("customize-cra");


module.exports = override(
    fixBabelImports("babel-plugin-import", {
        libraryDirectory: 'es',
        style: true
    }),
    /*addWebpackAlias({
        'react-dom$': 'react-dom/profiling',
    }),
    addLessLoader({
        javascriptEnabled: true,
        ident: 'postcss',
        sourceMap: true, // should skip in production
        importLoaders: true,
        localIdentName: '[name]__[local]___[hash:base64:5]'
    })*/
);