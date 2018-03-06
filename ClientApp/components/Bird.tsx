import * as React from 'react';
import { RouteComponentProps } from 'react-router';

interface CounterState {
    bird: Birdy;
    error: boolean;
}
export class Bird extends React.Component<RouteComponentProps<{}>, CounterState> {
    constructor() {
        super();
        
        this.state = { bird : {nameofbird:"" } , error: false };  

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);  
    }

    public render() {   
        return (
            <div>
            <form onSubmit={this.handleSubmit}>
            <h1>Linnun lisäys.</h1>
            <p>Syötä linnun nimi.</p>
            <p>
              <label>
                Nimi : 
                <input type="text" name="nameofbird" id="nameofbird" value={this.state.bird.nameofbird} onChange={this.handleChange} />
              </label>
              </p>
              <p>
              <input type="submit" value="Tallenna" />
              </p>
           </form>
            </div>
          );
    }
    handleChange(event :React.ChangeEvent<HTMLInputElement> ) {
        var value: string =String(event.target.value);
        let tmp :Birdy  = this.state.bird;   
        tmp.nameofbird = value;                        
        this.setState({bird:tmp});
      }
    
      handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();
        if (this.state.bird.nameofbird=="")
        {
            alert('Syötä linnun nimi!') ;
        }
        else
        {
            this.AddBird();
            this.setState({bird: {nameofbird:"" },error:false});
        }
      }

     
    private AddBird() {        

        fetch('api/SampleData/Birds', {  
          method: 'POST',
      headers: {
         'Accept': 'application/json',
        'content-type': 'application/json',
         'Data':'json'
            },          
      body: JSON.stringify({
          Birdy: this.state.bird,              
          })
      })      
      .then(handleErrors)
      .then(function(response) {
          console.log("ok");
      }).catch(function(error) {
          console.log(error);
      });


      function handleErrors(response : Response) {
        if (!response.ok) {           
            throw Error(response.statusText);
        }
        return response;
    }

}
}

interface Birdy{
    nameofbird : string;
}


