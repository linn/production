import { connect } from 'react-redux';
import {
    getItemError,
    getRequestErrors,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import PartFail from '../../components/partFails/PartFail';
import partFailActions from '../../actions/partFailActions';
import partFailSelectors from '../../selectors/partFailSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: partFailSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: partFailSelectors.getEditStatus(state),
    loading: partFailSelectors.getLoading(state),
    snackbarVisible: partFailSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.partFail.item),
    requestErrors: getRequestErrors(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(partFailActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: partFailActions.update,
    setEditStatus: partFailActions.setEditStatus,
    setSnackbarVisible: partFailActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(PartFail));
