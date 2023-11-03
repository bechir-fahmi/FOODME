export class MapLoaderService {
    private static promise: Promise<any>;
    public static load(): Promise<any> {
      let browserKey = "AIzaSyAKI3TuBMvR3SZUDhzpEwoR5MaK6mp5u5E";
      let map = {
        URL: 'https://maps.googleapis.com/maps/api/js?libraries=geometry,drawing&key=' + browserKey + '&callback=__onGoogleLoaded',      
      }
  
      // First time 'load' is called?
      if (!this.promise) {
  
        // Make promise to load
        this.promise = new Promise(resolve => {
          this.loadScript({ src: map.URL });        
          // Set callback for when google maps is loaded.
          (window as any)['__onGoogleLoaded' ] = ($event: any) => {
            resolve('google maps api loaded');
          };
        })
      }
  
      // Always return promise. When 'load' is called many times, the promise is already resolved.
      return this.promise;
    }
  
    //this function will work cross-browser for loading scripts asynchronously
    static loadScript({ src, callback }: { src: any; callback?: any; }): void {
      var s: any,
        r: boolean,
        t;
      r = false;
      s = document.createElement('script');
      s.type = 'text/javascript';
      s.src = src;
      s.onload = s.onreadystatechange = function () {
        //console.log( this.readyState ); //uncomment this line to see which ready states are called.
        if (!r && (!this.readyState || this.readyState == 'complete')) {
          r = true;
          if (typeof callback === "function")
            callback();
        }
      };
      t = document.getElementsByTagName('script')[0];
      if (t.parentNode){
        t.parentNode.insertBefore(s, t);
      }
   
    }
  }