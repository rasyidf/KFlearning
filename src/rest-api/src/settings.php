<?php
return [
    'settings' => [
        'displayErrorDetails' => true, // set to false in production
        'addContentLengthHeader' => false, // Allow the web server to send the content-length header

        // Renderer settings
        'renderer' => [
            'template_path' => __DIR__ . '/../templates/',
        ],

        // Monolog settings
        'logger' => [
            'name' => 'slim-app',
            'path' => isset($_ENV['docker']) ? 'php://stdout' : __DIR__ . '/../logs/app.log',
            'level' => \Monolog\Logger::DEBUG,
        ],

        // Main db
        'db' => [
            'host' => 'localhost',
            'user' => 'kodesian_web',
            'pass' => '9!W1-Sbop3',
            'main_db' => 'kodesian_kflearnig',
            'wp_db' => 'kodesian_web'
        ]
    ],
];
