import axios from 'axios';

export default class TournamentApi{
    constructor(){
        this.apiUrl = "/tournaments";
    }

    getTournaments = async () => {
        var result = await axios.get(this.apiUrl);
        return result.data;
    }

    getCategories = async () => {
        var result = await axios.get("/categories");
        return result.data;
    }

    getPools = async (tournament, category) => {
        const url = this.apiUrl + "/pools?id=" + tournament.tournamentId + "&category=" + category;
        var result = await axios.get(url);
        return result.data;
    }
}