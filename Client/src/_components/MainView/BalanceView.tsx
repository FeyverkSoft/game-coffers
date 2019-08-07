import * as React from "react";
import style from "./balanceview.module.less"
import { CanvasBlock, Grid, Col1, NamedValue } from "..";
import { Lang, GuildBalanceReport, LangF } from "../../_services";
import { IHolded } from "../../core";

interface IBalanceViewProps extends React.Props<any> {
    balance: GuildBalanceReport & IHolded;
    showGuildOperations(): void;
    [id: string]: any;
}
/// Плашка с информацией о пользователе
export const BalanceView = React.memo(({ ...props }: IBalanceViewProps) => {
    const { balance } = props;
    return <CanvasBlock
        title={Lang("MAIN_PAGE_MAIN_BALANCE")}
        type="important"
        isLoading={props.balance.holding}
    >
        <Grid
            direction="vertical"
        >
            <Col1>
                <NamedValue
                    name={Lang("MAIN_PAGE_GUILD_BALANCE")}>
                    <div
                        className={style['balance']}
                        onClick={() => props.showGuildOperations()}
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
    </CanvasBlock>
});