import * as actionTypes from '../actions';
import assemblyFailFaultCodesActions from '../actions/assemblyFailFaultCodesActions';

export default ({ dispatch }) => next => action => {
    const result = next(action);

    if (
        action.type ===
        actionTypes.assemblyFailFaultCodeActionTypes.RECEIVE_UPDATED_ASSEMBLY_FAIL_FAULT_CODE
    ) {
        dispatch(assemblyFailFaultCodesActions.fetch());
    }

    return result;
};
