/// <binding BeforeBuild='build' />
"use strict";

const $ = require('jquery');

const exec = require('child_process').exec;
const { series, src, dest, parallel } = require("gulp");
const rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");

const http = require('http'),
    express = require("express"),
    RED = require("node-red");

const paths = {
    webroot: "./wwwroot/"
};

paths.js =  "./wwwroot/js/**/*.js";
paths.minJs = "./wwwroot/js/**/*.min.js";
paths.css = "./wwwroot/css/**/*.css";
paths.minCss = "./wwwroot/css/**/*.min.css";
paths.concatJsDest = "./wwwroot/js/site.min.js";
paths.concatCssDest = "./wwwroot/css/site.min.css";

function clean(cb) {
    rimraf(paths.concatJsDest, cb);
    rimraf(paths.concatCssDest, cb);
}

function minJs() {
    return src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(dest("."));
}

function minCss() {
    return src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(dest("."));
}

async function minAsync() {
    await minCss();
    await minJs();
}

function runNodeRed() {
    try {
        // Create an Express app
        const app = express();

        // Add a simple route for static content served from 'public'
        app.use("/", express.static("public"));

        // Create a server
        const server = http.createServer(app);

        // Create the settings object - see default settings.js file for other options
        const settings = {
            httpAdminRoot: "/red",
            httpNodeRoot: "/api",
            userDir: "./wwwroot/node_red",
            functionGlobalContext: {}, // enables global context
            paletteCategories: ['B�sico','subflows', 'common', 'function', 'network', 'sequence', 'parser', 'storage'],
        };

        // Initialise the runtime with a server and settings
        RED.init(server, settings);

        // Serve the editor UI from /red
        app.use(settings.httpAdminRoot, RED.httpAdmin);

        // Serve the editor UI from /red
        app.use(settings.httpAdminRoot, RED.httpAdmin);

        // Serve the http nodes UI from /api
        app.use(settings.httpNodeRoot, RED.httpNode);

        // Serve the http nodes UI from /api
        server.listen(1880);
    }
    catch (err) {
        console.log(err);
    }
    finally {
        // Start the runtime
        RED.start();
        console.log(RED.settings.httpAdminRoot);
    }
}

exports.nodeRed = parallel(runNodeRed)
exports.build = series(clean, minAsync);