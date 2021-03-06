(function () {
    require.config({
        paths: {
            'jquery': '../libs/jquery',
            'q': '../libs/q',
            'http-requester': './http-requester',
            'username-validator': './username-validator',
            'ui-controller': './ui-controller',
            'controller': './controller',
            'sammy': '../libs/sammy-latest.min'
        }
    });

    require(['jquery','controller', 'sammy'], function ($, Controller, Sammy) {
        var controller = new Controller('http://crowd-chat.herokuapp.com/posts');
        controller.attachEventHandlers();

        var app = Sammy('#main-content', function () {
            this.get('#/login', function () {
                if (controller.isLoggedIn()) {
                    window.location = '#/chat';
                    return;
                }

                controller.loadLogin();
            });

            this.get('#/chat', function () {
                controller.viewAllMessages();
            });
        });

        console.log(app);

        app.run('#/login');
    });
}());