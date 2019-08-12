import configureMockStore from 'redux-mock-store';
import history from '../history';
import itemCreated from './itemCreated';

const mockStore = configureMockStore(itemCreated);
const payload = {
    data: {
        links: [
            {
                href: '/production/resources/board-fail-types/13',
                rel: 'self'
            }
        ]
    }
};

const store = mockStore({});

describe('When action type starts with RECEIVE_NEW_ but is not RECEIVE_NEW_SERIAL_NUMBER_REISSUE', () => {
    const action = {
        type: 'RECEIVE_NEW_ITEM',
        payload
    };

    it('redirects browser to created entities self href', () => {
        history.push = jest.fn();
        itemCreated()(store.dispatch)(action);
        expect(history.push).toHaveBeenCalledWith('/production/resources/board-fail-types/13');
    });
});

describe('When action type is RECEIVE_NEW_SERIAL_NUMBER_REISSUE', () => {
    const action = {
        type: 'RECEIVE_NEW_SERIAL_NUMBER_REISSUE',
        payload
    };

    it('should not redirect', () => {
        history.push = jest.fn();
        itemCreated()(store.dispatch)(action);
        expect(history.push).not.toHaveBeenCalled();
    });
});

describe('When action type is something else', () => {
    const action = {
        type: 'A_DIFFERENT_ACTION',
        payload
    };

    it('should not redirect', () => {
        history.push = jest.fn();
        itemCreated()(store.dispatch)(action);
        expect(history.push).not.toHaveBeenCalled();
    });
});
