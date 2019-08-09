import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import BoardFailTypes from '../../components/boardFailTypes/BoardFailTypes';
import boardFailTypesActions from '../../actions/boardFailTypesActions';
import boardFailTypesSelectors from '../../selectors/boardFailTypesSelectors';

const mapStateToProps = state => ({
    items: boardFailTypesSelectors.getItems(state),
    loading: boardFailTypesSelectors.getLoading(state),
    errorMessage: fetchErrorSelectors(state)
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
