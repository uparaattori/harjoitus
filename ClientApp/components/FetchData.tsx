import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface FetchDataExampleState {
    sightings: Sighting[];
    loading: boolean;
}

export class FetchData extends React.Component<RouteComponentProps<{}>, FetchDataExampleState> {
    constructor() {
        super();
        this.state = { sightings: [], loading: true };

        fetch('api/SampleData/Sightings')
            .then(response => response.json() as Promise<Sighting[]>)
            .then(data => {
                this.setState({ sightings: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderSightingsTable(this.state.sightings);

        return <div>
            <h1>Lintu havainnot.</h1>
            <p>Lista lintuhavainnoista.</p>
            { contents }
        </div>;
    }

    private static renderSightingsTable(sightings: Sighting[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Päivämäärä</th>
                    <th>Lintu</th>
                </tr>
            </thead>
            <tbody>
            {sightings.map(sighting =>
                <tr key={ sighting.sightingID }>
                    <td>{ sighting.dateFormatted }</td>
                    <td>{ sighting.birdName }</td>
                </tr>
            )}
            </tbody>
        </table>;
    }
}

interface Sighting {
    dateFormatted: string;
    birdName: string;
    sightingID: number;
}
