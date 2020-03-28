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
}
export const Loans = ({ ...props }: ILoans) => <div className={style['loans_wrapper']}>
    {
        Object.keys(props.loans).filter(_ => _ !== 'holding').map(_ => <Loan
            loan={props.loans[_]}
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
    onAChar?(id: string, userId: string): void;
}

export const Loan = React.memo(({ ...props }: ILoanProps) => {
    const { loan } = props;
    return <div key={loan.id} className={`${style['loan']}`}>
        <Tooltip
            className={`${style['loan']} ${style[loan.loanStatus.toLowerCase()]}`}
            title={`${formatDateTime(loan.expiredDate, 'd')} ${loan.description}`}
        // onClick={() => props.showLoanInfo(loan.id, loan.userId)}
        >
            {loan.amount}
        </Tooltip>
    </div>
});