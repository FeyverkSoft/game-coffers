import * as React from "react";
import { СanvasBlock, Grid, Col1, NamedValue } from "..";
import { Lang, DLang, GuildBalanceReport, LangF, GamersListView } from "../../_services";
import { BaseReactComp } from "../BaseReactComponent";
import { IHolded } from "../../core";

interface IGamerRowViewProps extends React.Props<any> {
    gamer: GamersListView & IHolded;
    [id: string]: any;
}
/// Плашка с информацией о пользователе
export class GamerRowView extends BaseReactComp<IGamerRowViewProps> {

    render() {
        const { gamer } = this.props;
        return (
            <Grid
                direction="vertical"
                key={gamer.id}
            >
                <Col1>
                    {gamer.characters}
                </Col1>
                <Col1>
                    {gamer.status}
                </Col1>
                <Col1>
                    {Lang(`TARIFF_ROLE_${gamer.rank}`)}
                </Col1>
                <Col1>
                    {gamer.penalties}
                </Col1>
                <Col1>
                    {gamer.balance}
                </Col1>
                <Col1>
                    {gamer.loans}
                </Col1>
            </Grid>
        );
    }
}