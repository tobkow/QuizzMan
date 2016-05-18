$(document)
    .ready(function () {
        var tocSticky = $('.toc .ui.sticky'),
            fullHeightContainer = $('.pusher > .full.height'),
            menu = $('#toc'),
            hideMenu = $('#toc .hide.item'),
            container = $('.main.container'),
            followMenu = container.find('.following.menu'),
            pageTabs = $('.masthead.tab.segment .tabs.menu .item'),
            footer = $('.page > .footer');

        // main sidebar
        menu.sidebar({
              dimPage: true,
              transition: 'overlay',
              mobileTransition: 'uncover'
        });
        // launch buttons
        menu.sidebar('attach events', '.launch.button, .view-ui, .launch.item');
        tocSticky.sticky({
            context: fullHeightContainer
        });
        // load page tabs
        if (pageTabs.size() > 0) {
            pageTabs.tab({
                  context: '.main.container',
                  childrenOnly: true,
                  history: false,
                  onFirstLoad: function () {
                      
                  },
                  onLoad: function () {
                      tocSticky.sticky('refresh');
                      //$(this).find('.ui.sticky').sticky('refresh');
                  }
              })
            ;
        }
        else {
            
        }
    });