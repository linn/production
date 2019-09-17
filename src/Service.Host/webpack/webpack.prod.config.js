const path = require('path');
const MomentLocalesPlugin = require('moment-locales-webpack-plugin');

function localResolve(preset) {
    return Array.isArray(preset)
        ? [require.resolve(preset[0]), preset[1]]
        : require.resolve(preset);
}
module.exports = {
    entry: {
        app: ['babel-polyfill', './client/src/index.js'],
        'silent-renew': './client/silent-renew/index.js'
    },
    output: {
        path: path.resolve(__dirname, '../client/build'), // string
        filename: '[name].js',
        publicPath: '/production/maintenance/build/'
    },
    module: {
        rules: [
            {
                exclude: [/\.html$/, /\.(js|jsx)$/, /\.css$/, /\.scss$/, /\.json$/, /\.svg$/],
                use: {
                    loader: 'url-loader',
                    query: {
                        limit: 10000,
                        name: 'media/[name].[hash:8].[ext]'
                    }
                }
            },
            {
                test: /\.js$/,
                use: {
                    loader: 'babel-loader',
                    query: {
                        presets: [
                            ['@babel/preset-env', { modules: 'commonjs' }],
                            '@babel/preset-react'
                        ].map(localResolve),
                        plugins: ['@babel/plugin-transform-runtime'].map(localResolve)
                    }
                },
                exclude: /node_modules/
            },
            {
                test: /\.css$/,
                use: [
                    'style-loader',
                    {
                        loader: 'css-loader',
                        options: {
                            importLoaders: 1
                        }
                    },
                    'postcss-loader'
                ]
            },
            {
                test: /\.scss$/,
                use: [
                    'style-loader',
                    {
                        loader: 'css-loader',
                        options: {
                            importLoaders: 1
                        }
                    },
                    'sass-loader',
                    'postcss-loader'
                ]
            },
            {
                test: /\.svg$/,
                use: {
                    loader: 'file-loader',
                    query: {
                        name: 'media/[name].[hash:8].[ext]'
                    }
                }
            }
        ]
    },

    resolve: {
        alias: {
            moment: path.resolve('./node_modules/moment'),
            '@material-ui/pickers': path.resolve('./node_modules/@material-ui/pickers'),
            '@material-ui/styles': path.resolve('./node_modules/@material-ui/styles'),
            'react-redux': path.resolve('./node_modules/react-redux'),
            react: path.resolve('./node_modules/react'),
            notistack: path.resolve('./node_modules/notistack')
        }
        //modules: [path.resolve('node_modules'), 'node_modules'].concat(/* ... */)
    },
    plugins: [
        //new BundleAnalyzerPlugin(),
        // To strip all locales except “en”
        new MomentLocalesPlugin()
    ],
    devtool: 'cheap-module-eval-source-map'
    // enhance debugging by adding meta info for the browser devtools
    // source-map most detailed at the expense of build speed.
};
