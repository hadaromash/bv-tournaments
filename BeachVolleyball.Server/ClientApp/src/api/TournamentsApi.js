import axios from 'axios';

export default class TournamentApi{
    getTournaments = async () => {
        var result = await axios.get("/tournaments");
        return result.data;
    }
}