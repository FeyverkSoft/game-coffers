import * as React from "react";
import style from "./loans.module.scss";
import { PlusCircleFilled } from '@ant-design/icons';
import { Private } from "../Private";
import { ILoanView } from "../../_services/gamer/GamersListView";
import { Tooltip, Button } from "antd";
import { Lang } from "../../_services";
import { Dictionary } from "../../core";
import { formatDateTime } from "../../_helpers";

interface ILoans {
    loans: Dictionary<ILoanView>;
    userId: string;
    onAddLoan(userId: string): void;
    onLoanShow(userId: string, loanId: string): void;
}
export const Loans = ({ ...props }: ILoans) => <div className={style['loans_wrapper']}>
    {
        Object.keys(props.loans).filter(_ => _ !== 'holding').map(_ => <Loan
            key={props.loans[_].id}
            loan={props.loans[_]}
            onClick={(loanId) => props.onLoanShow(loanId, props.loans[_].id)}
        />)
    }
    <Private roles={['admin', 'leader', 'officer']}>
        <Tooltip title={Lang('NEW_LOAN_MODAL')}>
            <Button
                type="link"
                className={style['add']}
                icon={<PlusCircleFilled />}
                onClick={() => props.onAddLoan(props.userId)}
            />
        </Tooltip>
    </Private>
</div>;

interface ILoanProps {
    loan: ILoanView;
    onClick(loanId: string): void;
}

export const Loan = React.memo(({ ...props }: ILoanProps) => {
    const { loan } = props;
    return <div
        key={loan.id}
        className={`${style['loan']}`}
        onClick={() => props.onClick(loan.id)}>
        <Tooltip
            className={`${style['loan']} ${style[loan.loanStatus.toLowerCase()]}`}
            title={`${formatDateTime(loan.expiredDate, 'd')} ${loan.description}`}
        >
            <span>{loan.amount}</span>
        </Tooltip>
    </div>
});