import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import BoardFailTypes from '../../components/boardFailTypes/BoardFailTypes';
import boardFailTypesActions from '../../actions/boardFailTypesActions';
import boardFailTypesSelectors from '../../selectors/boardFailTypesSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    items: boardFailTypesSelectors.getItems(state),
    loading: boardFailTypesSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.boardFailTypes.item)
});

const initialise = () => dispatch => {
    dispatch(boardFailTypesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(BoardFailTypes));
