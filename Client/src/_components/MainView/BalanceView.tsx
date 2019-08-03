import * as React from "react";
import style from "./balanceview.module.less"
import { СanvasBlock, Grid, Col1, NamedValue } from "..";
import { Lang, GuildBalanceReport, LangF } from "../../_services";
import { BaseReactComp } from "../BaseReactComponent";
import { IHolded } from "../../core";

interface IBalanceViewProps extends React.Props<any> {
    balance: GuildBalanceReport & IHolded;
    showGuildOperations(): void;
    [id: string]: any;
}
/// Плашка с информацией о пользователе
export class BalanceView extends BaseReactComp<IBalanceViewProps> {

    render() {
        const { balance } = this.props;
        return (<СanvasBlock
            title={Lang("MAIN_PAGE_MAIN_BALANCE")}
            type="important"
            isLoading={this.props.balance.holding}
        >
            <Grid
                direction="vertical"
            >
                <Col1>
                    <NamedValue
                        name={Lang("MAIN_PAGE_GUILD_BALANCE")}>
                        <div
                            className={style['balance']}
                            onClick={() => this.props.showGuildOperations()}
                        >
                            {LangF("MAIN_PAGE_GUILD_B_F", balance.balance, balance.gamersBalance, balance.balance + balance.gamersBalance)}
                        </div>
                    </NamedValue>
                </Col1>
                <Col1>
                    <NamedValue name={Lang("MAIN_PAGE_GUILD_LOANS")}>
                        {balance.activeLoansAmount}
                    </NamedValue>
                </Col1>
                <Col1>
                    <NamedValue name={Lang("MAIN_PAGE_EXPECTED_TAX")}>
                        {LangF("MAIN_PAGE_EXPECTED_TAX_FORMAT", balance.taxAmount, balance.expectedTaxAmount)}
                    </NamedValue>
                </Col1>
            </Grid>
        </СanvasBlock>
        );
    }
}