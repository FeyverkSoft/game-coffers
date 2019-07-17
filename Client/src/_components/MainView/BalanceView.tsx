import * as React from "react";
import { СanvasBlock, Grid, Col1, NamedValue } from "..";
import { Lang, IGuild, DLang } from "../../_services";
import { BaseReactComp } from "../BaseReactComponent";

interface IBalanceViewProps extends React.Props<any> {
    guildInfo: IGuild;
    isLoading?: boolean;
    [id: string]: any;
}
/// Плашка с информацией о пользователе
export class BalanceView extends BaseReactComp<IBalanceViewProps> {

    render() {
        const { guildInfo } = this.props;

        return (<СanvasBlock
            title={Lang("MAIN_PAGE_MAIN_INFO")}
            type="important"
            isLoading={this.props.isLoading}
        >
            <Grid
                direction="horizontal"
            >

                <Col1>
                    <NamedValue name={Lang("MAIN_PAGE_GUILD_BALANCE")}>
                        {guildInfo.balance || 0}
                    </NamedValue>
                </Col1>
                <Col1>
                    <NamedValue name={Lang("MAIN_PAGE_GUILD_LOANS")}>
                        {0}
                    </NamedValue>
                </Col1>
                <Col1>
                    <NamedValue name={Lang("MAIN_PAGE_EXPECTED_TAX")}>
                        {0}
                    </NamedValue>
                </Col1>
            </Grid>
        </СanvasBlock>
        );
    }
}