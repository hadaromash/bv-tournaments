import axios from 'axios';

export default class PlayerApi{
    getPhoto = async (playerId) => {
        var result = await axios.get("/players/photo/" + playerId);
        return result.data;
    }
}