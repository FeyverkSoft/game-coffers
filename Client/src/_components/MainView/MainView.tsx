import * as React from "react";
import { CanvasBlock, Grid, Col1, NamedValue } from "..";
import { Lang, IGuild, DLang } from "../../_services";
import { BaseReactComp } from "../BaseReactComponent";

interface IMainViewProps extends React.Props<any> {
    guildInfo: IGuild;
    isLoading?: boolean;
    [id: string]: any;
}
/// Плашка с информацией о пользователе
export class MainView extends BaseReactComp<IMainViewProps> {

    render() {
        const { guildInfo } = this.props;

        return (<CanvasBlock
            title={Lang("MAIN_PAGE_MAIN_INFO")}
            type="important"
            isLoading={this.props.isLoading}
        >
            <Grid
                direction="vertical"
            >
                <Col1>
                    <NamedValue name={Lang("MAIN_PAGE_CHARACTERS_COUNT")}>
                        {guildInfo.charactersCount || 0}
                    </NamedValue>
                </Col1>
                <Col1>
                    <NamedValue name={Lang("MAIN_PAGE_GAMERS_COUNT")}>
                        {guildInfo.gamersCount || 0}
                    </NamedValue>
                </Col1>
                <Col1>
                    <NamedValue name={Lang("MAIN_RECRUITMENTSTATUS")}>
                        {DLang('RECRUITMENTSTATUS', guildInfo.recruitmentStatus)}
                    </NamedValue>
                </Col1>
            </Grid>
        </CanvasBlock>
        );
    }
}