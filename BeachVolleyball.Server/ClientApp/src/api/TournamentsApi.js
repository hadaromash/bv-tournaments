import axios from 'axios';

export default class TournamentApi{
    getTournaments = async () => {
        var result = await axios.get("/tournaments");
        return result.data;
    }

    getCategories = async (tournamentId) => {
        var result = await axios.get("/categories?tournamentId=" + tournamentId);
        return result.data;
    }

    getPools = async (tournament, category) => {
        var result = await axios.get("/pools", {
            params: {
                tournamentId: tournament.tournamentId,
                categoryId: category.id,
                categoryName: category.displayName
            }
        });
        return result.data;
    }
}