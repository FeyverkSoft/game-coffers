const CACHE = 'daddy_and_co-v1';
const timeout = 5500;
var assets=[];

const register = () => {
    let isLocalhost = false;
    if (!('serviceWorker' in navigator)) {
        console.warn('ServiceWorker unavailable');
        return;
    }
    if (isLocalhost) {
        console.warn('ServiceWorker unavailable from localhost');
        return;
    }

    const swUrl = `./serviceWorker.js`;
    navigator.serviceWorker
        .register(swUrl)
        .then(() => navigator.serviceWorker.ready.then(
            (worker) => {
                worker.sync.register('syncdata');
            }))
        .catch((err) => console.log(err));
}

// Временно-ограниченный запрос.
function fromNetwork(request, timeout) {
    return new Promise((fulfill, reject) => {
        var timeoutId = setTimeout(reject, timeout);
        fetch(request).then((response) => {
            clearTimeout(timeoutId);
            fulfill(response);
        }, reject);
    });
}

function fromCache(request) {
    // Открываем наше хранилище кэша (CacheStorage API), выполняем поиск запрошенного ресурса.
    // Обратите внимание, что в случае отсутствия соответствия значения Promise выполнится успешно, но со значением `undefined`
    return caches.open(CACHE).then((cache) =>
        cache.match(request).then((matching) =>
            matching || Promise.reject('no-match')
        ));
}


/* eslint-disable */
self.addEventListener('install', (event) => {
    event.waitUntil(
        fetch('./asset-manifest.json')
            .then(response => {
                if (response.status == 200) {
                    return response.json();
                }
                return Promise.resolve();
            })
            .then(body => {
                if (body) {
                    let files = body.files;

                    let cashedFiles = Object.keys(files)
                        .filter(key => !key.includes(".map"))
                        .map(f => files[f]);

                    ["./", "./birthday", "./logout", "./auth"].forEach(x => {
                        cashedFiles.push(x);
                    })

                    event.waitUntil(
                        caches
                            .open(CACHE)
                            .then((cache) => cache.addAll(cashedFiles))
                            .then(() => self.skipWaiting())
                    );
                }
            }));


    console.log('Установлен');
});

/* eslint-disable */
self.addEventListener('activate', (event) => {
    console.log('Активирован');
    event.waitUntil(self.clients.claim());
});

const FALLBACK =
    '<div>\n' +
    '    <div>Daddy And Co</div>\n' +
    '    <div>you are offline</div>\n' +
    '</div>';

function useFallback() {
    return Promise.resolve(new Response(FALLBACK, {
        headers: {
            'Content-Type': 'text/html; charset=utf-8'
        }
    }));
}


/* eslint-disable */
self.addEventListener('fetch', (event) => {
    event.respondWith(fromNetwork(event.request, timeout)
        .catch((err) => {
            console.log(`Error: ${err}`);
            if (event.request.destination == "document" ||
                event.request.mode == "navigate"
            )
                return fromCache(event.request) || useFallback();
            throw err;
        }));
});