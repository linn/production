import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import AteFaultCode from '../../components/ate/AteFaultCode';
import ateFaultCodeActions from '../../actions/ateFaultCodeActions';
import ateFaultCodeSelectors from '../../selectors/ateFaultCodeSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: ateFaultCodeSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: ateFaultCodeSelectors.getEditStatus(state),
    loading: ateFaultCodeSelectors.getLoading(state),
    snackbarVisible: ateFaultCodeSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.ateFaultCode.item)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(ateFaultCodeActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: ateFaultCodeActions.update,
    setEditStatus: ateFaultCodeActions.setEditStatus,
    setSnackbarVisible: ateFaultCodeActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(AteFaultCode));
