import * as React from 'react';
import { RouteComponentProps } from 'react-router';

interface CounterState {
    birds: Bird[];
    loading: boolean;
}

export class Counter extends React.Component<RouteComponentProps<{}>, CounterState> {
    constructor() {
        super();
        
        this.state = { birds: [], loading: true };

        fetch('api/SampleData/Birds')
            .then(response => response.json() as Promise<Bird[]>)
            .then(data => {
                this.setState({ birds: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderBirdsTable(this.state.birds);
        return <div>
            <h1>Lisää havainto.</h1>

            <p>Voit lisätä havainnon painamalla lisää nappia.</p>

            {contents}
        </div>;
    }

    private incrementCounter(_birdId:number) {        
              fetch('api/SampleData/Sightings/'+_birdId, {  
                method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                },
            body: JSON.stringify({
                BirdId: _birdId,                
                })
            })

            var tmpBirds:Bird[]=[];

            this.state.birds.forEach(element => {
                if (element.birdID==_birdId)
                {
                    element.count= element.count+1
                }
                tmpBirds.push(element);
            });

            this.setState({birds:tmpBirds,loading:false});
    }

    private renderBirdsTable(birds: Bird[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Lintu</th>
                    <th>Lukumäärä</th>
                    
                </tr>
            </thead>
            <tbody>
            {birds.map(bird =>
                <tr key={ bird.birdID }>
                    <td>{ bird.birdName }</td>
                    <td>{ bird.count }</td>
                    <td> <button onClick={ () => { this.incrementCounter(bird.birdID) } }>Lisää</button></td>
                </tr>
            )}
            </tbody>
        </table>;
    }
}

interface Bird {
    count: number;
    birdName: string;
    birdID: number;
}

